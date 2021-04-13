﻿using Domain.Dto;
using Domain.Interface.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/produtos")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<ProductDto> products = await _service.GetAll();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> Get(int code)
        {
            try
            {
                ProductDto product = await _service.GetByCode(code);

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto product = await _service.Save(productDto);

                if (product.DateRegister.Date < DateTime.Now.Date)
                {
                    return NoContent();
                }

                return CreatedAtAction(nameof(Get), new { code = product.Code }, productDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{path}")]
        public async Task<IActionResult> Post(string path)
        {
            try
            {
                List<ProductDto> products = await _service.ReadDocument(path);

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
