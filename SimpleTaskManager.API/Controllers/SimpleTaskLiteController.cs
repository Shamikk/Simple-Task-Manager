using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleTaskManager.API.DAL;
using SimpleTaskManager.API.Model.BLL;
using SimpleTaskManager.API.Model.BLL.Repository;
using SimpleTaskManager.API.Model.DAL;
using SimpleTaskManager.API.Model.Dto;

namespace SimpleTaskManager.API.Controllers
{
    [Route("api/[controller]/lite")]
    [ApiController]
    public class SimpleTaskLiteController : ControllerBase
    {
        private readonly MapperConfiguration _mapperConfiguration = null;
        private readonly IMapper _iMapper = null;
        private readonly ISimpleTaskRepository _simpleTaskRepository;

        public SimpleTaskLiteController(ISimpleTaskRepository simpleTaskRepository)
        {
            _simpleTaskRepository = simpleTaskRepository;

            // something's wrong here, can't tell what exactly... need to create a bug :P
            //_mapperConfiguration = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<SimpleTask, SimpleTaskLiteDto>()
            //        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //        .ForMember(dest => dest.NumberOfUpdates, opt => opt.MapFrom(src => src.NumberOfUpdates));
            //});
        }


        // GET: api/<controller>/lite
        [HttpGet]
        public ActionResult<IEnumerable<SimpleTaskLiteDto>> Get()
        {
            var tasks = _simpleTaskRepository.GetAll();

            var tasksDto = tasks.Select(x => new SimpleTaskLiteDto
            {
                Name = x.Name,
                NumberOfUpdates = x.NumberOfUpdates
            }).ToList();

            return Ok(tasksDto);
        }
    }
}