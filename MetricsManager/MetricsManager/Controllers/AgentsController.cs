using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsManager.Controllers
{
    // При компиляции путь, например, такой: api/agents/register
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly AgentInfoHolder _holder;

        /// <summary>
        /// Зарегистрировать агент метрик
        /// </summary>
        /// <param Имя="agentInfo"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _holder.AddValue(agentInfo);
            return Ok();
        }

        /// <summary>
        /// Активировать агент метрик
        /// </summary>
        /// <param Имя="agentId"></param>
        /// <returns></returns>
        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }

        /// <summary>
        /// Дезактивировать агент метрик
        /// </summary>
        /// <param name="agentId"></param>
        /// <returns></returns>
        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }

    }
}
