﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RMS_API.Data;

namespace RMS_API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(ICourseRepository courseRepository, ILogger<CoursesController> logger)
        {
            _courseRepository = courseRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var (result, count) = _courseRepository.GetAll();

                if (count == 0)
                    return NotFound();

                return Ok(new { Results = result, Count = count });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get courses: {ex}");
                return BadRequest("Failed to get courses");
            }

        }
    }
}