using AutoMapper;
using BatchAPI.Models;
using BatchAPI.Models.Dtos;
using BatchAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private IBatchRepository _btRepo;
        private readonly IMapper _mapper;
        public BatchController(IBatchRepository btRepo, IMapper mapper)
        {
            _btRepo = btRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get the List of Batches
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetBatch()
        {
            var objList = _btRepo.GetBatches();
            var objDto = new List<BatchDtos>();

            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<BatchDtos>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        ///Get Individual Batch      
        /// </summary>
        /// <param name="batchId">The Id of the Batch</param>
        /// <returns></returns>
        [HttpGet("{batchid:guid}", Name = "GetBatch")]
        public IActionResult GetBatch(Guid batchId)
        {
            var obj = _btRepo.GetBatch(batchId);
            if (obj==null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<BatchDtos>(obj);
            return Ok(objDto);
       
        }
        /// <summary>
        /// This Adds a new Batch Entry
        /// </summary>
        /// <param name="batchDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateBatch([FromBody] BatchDtos batchDto)
        {
            if (batchDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_btRepo.BatchExists(batchDto.businessUnit))
            {
                ModelState.AddModelError("", "Business Unit Exists!!!");
                return StatusCode(404, ModelState);
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var btObj = _mapper.Map<Batch>(batchDto);
            if (!_btRepo.CreateBatch(btObj))
            {
                ModelState.AddModelError("", $"Something went wroing when saving the Record{btObj.businessUnit}");
                return StatusCode(500, ModelState); 
            }
            //return Ok();

            return CreatedAtRoute("GetBatch", new { batchid = btObj.batchID }, btObj);
        }
        /// <summary>
        /// This is Used to make changes to a batch
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="batchDto"></param>
        /// <returns></returns>
        [HttpPatch("{batchid:guid}", Name = "Updatebatch")]
        public IActionResult Updatebatch(Guid batchId,[FromBody] BatchDtos batchDto)
        {
            if (batchDto == null || batchId!=batchDto.batchID)
            {
                return BadRequest(ModelState);
            }
            //if (_btRepo.BatchExists(batchDto.businessUnit))
            //{
            //    ModelState.AddModelError("", "Business Unit Exists!!!");
            //    return StatusCode(404, ModelState);
            //}
            var btObj = _mapper.Map<Batch>(batchDto);
            if (!_btRepo.UpdateBatch(btObj))
            {
                ModelState.AddModelError("", $"Something went wroing when Updating the Record{btObj.businessUnit}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        /// <summary>
        /// This Deletes the given Batch
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        [HttpDelete("{batchid:guid}", Name = "Deletebatch")]
        public IActionResult Deletebatch(Guid batchId )
        {
            if (!_btRepo.BatchExists(batchId))
            {
                return NotFound();
            }

            var btObj = _btRepo.GetBatch(batchId);
            if (!_btRepo.DeleteBatch(btObj))
            {
                ModelState.AddModelError("", $"Something went wroing when Deleting the Record{btObj.businessUnit}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
