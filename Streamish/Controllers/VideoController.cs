using System;
using Microsoft.AspNetCore.Mvc;
using Streamish.Repositories;
using Streamish.Models;

namespace Streamish.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoRepository _videoRepository;
        public VideoController(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_videoRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var video = _videoRepository.GetById(id);
            if (video == null)
            {
                return NotFound();
            }
            return Ok(video);
        }

        [HttpGet("GetAllWithComments")] //this names your route in url
        public IActionResult GetWithComments() //this can be named anything
        {
            var videos = _videoRepository.GetAllWithComments();
            return Ok(videos);
        }

        [HttpGet("VideoWithComments")]
        public IActionResult GetVideoWithComments(int id)
        {
            var video = _videoRepository.GetVideoByIdWithComments(id);
            return Ok(video);
        }

        [HttpPost] //posting BRAND NEW info to the site
        public IActionResult Post(Video video)
        {
            _videoRepository.Add(video);
            return CreatedAtAction("Get", new { id = video.Id }, video);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Video video)
        {
            if (id != video.Id)
            {
                return BadRequest();
            }

            _videoRepository.Update(video);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _videoRepository.Delete(id);
            return NoContent();
        }

        [HttpGet("search")]
        public IActionResult Search(string guac, bool sortDesc)
        {
            return Ok(_videoRepository.Search(guac, sortDesc));
        }
    }
}