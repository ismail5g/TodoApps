﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Todo.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Todo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        // GET: api/<TodoController>
        [HttpGet]
        public IEnumerable<TodoItemModel> Get()
        {
            return new TodoItemModel().GetAll();
        }

        // GET api/<TodoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemModel>> Get(int id)
        {
            var todoItem = new TodoItemModel().Get(id);
             if (todoItem == null)
             {
                 return NotFound();
             }

             return todoItem;
        }

        // POST api/<TodoController>
        [HttpPost]
        public async Task<ActionResult<TodoItemModel>> Post(TodoItemModel item)
        {
            item.Add(item);
            return CreatedAtAction(nameof(Get), new {id = item.Id});
        }

        // PUT api/<TodoController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TodoItemModel item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }


            var todoItem = item.Get(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            
            try
            {
                item.Update(id, item);
            }
            catch (DbUpdateConcurrencyException) when (!item.Exits(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE api/<TodoController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = new TodoItemModel();
            var todoItem = model.Get(id);

            if (todoItem == null)
            {
                return NotFound();
            }
            model.Remove(id);
            return NoContent();
        }
    }
}
