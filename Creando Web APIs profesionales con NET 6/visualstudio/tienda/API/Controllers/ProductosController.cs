﻿using API.DTO;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace API.Controllers
{

    public class ProductosController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductosController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductoListDTO>>> Get()
        {
            var productos = await _unitOfWork.Productos.GetAllAsync();
            return _mapper.Map<List<ProductoListDTO>>(productos); 
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductoDTO>> Get(int id)
        {

            var producto = await _unitOfWork.Productos.GetByIdAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            return _mapper.Map<ProductoDTO>(producto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Producto>> Post(ProductoAddUpdateDTO productoDTO)
        {
            var producto = _mapper.Map<Producto>(productoDTO);

            _unitOfWork.Productos.Add(producto);
            await _unitOfWork.SaveAsync();
            if (producto == null)
            {
                return BadRequest();
            }

            productoDTO.Id = producto.Id;
            return CreatedAtAction(nameof(Post), new { id = productoDTO.Id }, productoDTO);

        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductoAddUpdateDTO>> Put(int id, [FromBody] ProductoAddUpdateDTO productoDTO)
        {

            if (productoDTO == null)
            {
                return NotFound();
            }

            var producto = _mapper.Map<Producto>(productoDTO); 

            _unitOfWork.Productos.Update(producto);
            await _unitOfWork.SaveAsync();


            return productoDTO;

        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Producto>> Delete(int id)
        {

            var producto = await _unitOfWork.Productos.GetByIdAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _unitOfWork.Productos.Remove(producto);
            await _unitOfWork.SaveAsync();


            return NoContent();

        }
    }
}
