﻿using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Api.Models.Abstracted
{
    public interface ICRUDControllers<T>
    {
        [HttpGet]
        public ActionResult<List<T>> GetAll();
        [HttpGet("{id}")]
        public ActionResult<T> GetById(int id);
        [HttpPost]
        public IActionResult Create([FromBody]T model);
        [HttpPut("{id}")]
        public IActionResult Update(int id,T model);
        [HttpDelete("{id}")]
        public IActionResult Delete(int id);
    }
}
