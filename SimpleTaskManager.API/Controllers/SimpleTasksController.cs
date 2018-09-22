using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleTaskManager.API.DAL;
using SimpleTaskManager.API.Model.BLL;
using SimpleTaskManager.API.Model.BLL.External;
using SimpleTaskManager.API.Model.BLL.Repository;
using SimpleTaskManager.API.Model.DAL;
using SimpleTaskManager.API.Model.Dto;
using SimpleTaskManager.Core.BLL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleTaskManager.API.Controllers
{
    [Route("api/[controller]")]
    public class SimpleTasksController : ControllerBase
    {
        private readonly MapperConfiguration _mapperConfiguration = null;
        private readonly IMapper _iMapper = null;
        private static int _numberOfUpsertOperations = 0;
        private readonly IValidatorService _validatorService;
        private readonly STMContext _context;
        private static HashSet<string> _tasksInProgress;
        private readonly ISimpleTaskRepository _simpleTaskRepository;

        public SimpleTasksController(ISimpleTaskRepository simpleTaskRepository)
        {
            _simpleTaskRepository = simpleTaskRepository;

            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SimpleTask, SimpleTaskDto>();
                cfg.CreateMap<SimpleTaskDto, SimpleTask>();
                cfg.CreateMap<SimpleTaskBaseDto, SimpleTask>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Status.Open))
                .ForMember(dest => dest.CreationDateTime, opt => opt.MapFrom(src => DateTime.Now));
            });

            _iMapper = _mapperConfiguration.CreateMapper();
            _numberOfUpsertOperations = _simpleTaskRepository.GetAll().Sum(x => x.NumberOfUpdates);
            _validatorService = new ValidatorService();

            var taskNames = _simpleTaskRepository.GetAll().Select(x => x.Name);
            TheBestTaskNameCheckerInTheWorld.SyncNames(taskNames);

            // maybe this should be a singleton...? 
            if (_tasksInProgress == null)
                _tasksInProgress = new HashSet<string>();
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<SimpleTaskDto>> Get()
        {
            var tasksFromRepo = _simpleTaskRepository.GetAll()
                            .Select(x => _iMapper.Map<SimpleTask, SimpleTaskDto>(x))
                            .ToList();
            return Ok(tasksFromRepo);
        }

        // GET api/<controller>/5
        [HttpGet("{name}")]
        public ActionResult<SimpleTaskDto> Get(string name)
        {
            var task = _simpleTaskRepository.Get(name);
            if (task == null)
                return NotFound(name);

            var taskDto = _iMapper.Map<SimpleTask, SimpleTaskDto>(task);
            return Ok(taskDto);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]SimpleTaskBaseDto value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isTaskInProgress = true;
            do
            {
                isTaskInProgress = _tasksInProgress.Any(x => x == value.Name);
                if (isTaskInProgress)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    _tasksInProgress.Add(value.Name);
                }

            } while (!isTaskInProgress);

            var task = _iMapper.Map<SimpleTaskBaseDto, SimpleTask>(value);
            task.LastUpdateBy = task.CreatedBy;
            task.LastUpdateDateTime = task.CreationDateTime;

            bool isValid = await Validate(task);

            if (!isValid)
            {
                _tasksInProgress.Remove(value.Name);
                return BadRequest($"Task with name {task.Name} already exists in database.");
            }

            _simpleTaskRepository.Add(task);
            IncrementNumberOfTotalOperations();
            _tasksInProgress.Remove(value.Name);

            return Ok(value);
        }

        // PUT api/<controller>/5
        [HttpPut("{name}")]
        public async Task<ActionResult> Put(string name, [FromBody]SimpleTaskDto value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isTaskInProgress = true;
            do
            {
                isTaskInProgress = _tasksInProgress.Any(x => x == value.Name);
                if (isTaskInProgress)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    _tasksInProgress.Add(value.Name);
                }

            } while (!isTaskInProgress);

            var task = _iMapper.Map<SimpleTaskDto, SimpleTask>(value);
            SimpleTask existingTask = null;

            existingTask = _simpleTaskRepository.Get(name);
            if (existingTask == null)
                return NotFound($"Task with name {name} does not exist.");

            // check for uniqness of name
            bool isValid = await Validate(existingTask);

            if (!isValid)
            {
                _tasksInProgress.Remove(value.Name);
                return BadRequest($"Task name {name} already exists in database.");
            }

            // Actually, this should not be used at all - PUT is just to take whole object and "put" it somewhere else. That's the theory at least :)
            // No fields should be changed, including last update time and number of updates.
            // For updating task please use "PATCH".

            existingTask.LastUpdateDateTime = DateTime.Now;
            existingTask.LastUpdateBy = "Mikk";
            existingTask.NumberOfUpdates++;

            existingTask.Update(task);
            _simpleTaskRepository.Update(task);
            IncrementNumberOfTotalOperations();
            _tasksInProgress.Remove(value.Name);

            return Ok(existingTask);
        }

        // PATCH api/<controller>/5
        [HttpPatch("{name}")]
        public async Task<ActionResult> Patch(string name, [FromBody]SimpleTaskDto value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isTaskInProgress = true;
            do
            {
                isTaskInProgress = _tasksInProgress.Any(x => x == value.Name);
                if (isTaskInProgress)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    _tasksInProgress.Add(value.Name);
                }

            } while (!isTaskInProgress);

            var task = _iMapper.Map<SimpleTaskDto, SimpleTask>(value);
            SimpleTask existingTask = null;

            existingTask = _simpleTaskRepository.Get(name);
            if (existingTask == null)
            {
                _tasksInProgress.Remove(value.Name);
                return NotFound($"Task with name {name} does not exist.");
            }
                

            bool isValid = await Validate(existingTask);
            if (!isValid)
            {
                _tasksInProgress.Remove(value.Name);
                return BadRequest($"Task with name {name} already exists in database.");
            }
                

            existingTask.LastUpdateDateTime = DateTime.Now;
            existingTask.LastUpdateBy = "Mikk";
            existingTask.NumberOfUpdates++;

            existingTask.Patch(task);
            _simpleTaskRepository.Update(task);
            IncrementNumberOfTotalOperations();
            _tasksInProgress.Remove(value.Name);

            return Ok(existingTask);
        }

        private void IncrementNumberOfTotalOperations()
        {
            _numberOfUpsertOperations++;
        }

        private Task<bool> Validate(SimpleTask simpleTask)
        {
            return new TaskFactory().StartNew(() => _validatorService.Validate(simpleTask));
        }
    }
}
