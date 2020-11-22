using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoList.API.ViewModels;
using TodoList.Application.Interfaces;
using TodoList.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoList.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private ITodoListService todoListService;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<TodoListController> logger;

        public TodoListController(
            ITodoListService todoListService,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ILogger<TodoListController> logger)
        {
            this.todoListService = todoListService;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }

        // GET: api/<TodoListController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var username = httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.Name).Value;

            var result = await todoListService.GetTodoItemList(username);
            if (result.Success)
            {
                var listViewModels = mapper.Map<List<TodoItemViewModel>>(result.Data);
                return Ok(listViewModels);
            }

            return BadRequest(result.Message);
        }

        // GET api/<TodoListController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await todoListService.GetTodoItem(id);
            if (result.Success)
            {
                var itemViewModels = mapper.Map<TodoItemViewModel>(result.Data);
                return Ok(itemViewModels);
            }

            return BadRequest(result.Message);
        }

        // POST api/<TodoListController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]TodoItemViewModel todoItemViewModel)
        {
            var username = httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.Name).Value;

            var item = mapper.Map<TodoItem>(todoItemViewModel);
            var result = await todoListService.AddTodoItem(username, item);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        // PUT api/<TodoListController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]TodoItemViewModel todoItemViewModel)
        {
            var username = httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.Name).Value;

            var item = mapper.Map<TodoItem>(todoItemViewModel);

            var result = await todoListService.UpdateTodoItem(id, username, item);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        // DELETE api/<TodoListController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await todoListService.RemoveTodoItem(id);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
    }
}
