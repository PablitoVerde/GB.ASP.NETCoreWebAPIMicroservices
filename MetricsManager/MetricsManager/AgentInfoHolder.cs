using System.Collections.Concurrent;

namespace MetricsManager
{
    public class AgentInfoHolder
    {
        public ConcurrentDictionary<int, AgentInfo> valuesHolder;

        public void AddValue(AgentInfo agentInfo)
        {
            valuesHolder.TryAdd(agentInfo.AgentId, agentInfo);
        }
    }
}
