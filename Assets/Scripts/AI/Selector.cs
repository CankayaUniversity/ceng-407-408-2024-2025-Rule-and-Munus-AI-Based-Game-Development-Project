using System.Collections.Generic;

namespace BehaviorTree
{
    public class Selector : Node
    {
        private List<Node> nodes;

        public Selector(List<Node> nodes)
        {
            this.nodes = nodes;
        }

        public override bool Evaluate()
        {
            foreach (Node node in nodes)
            {
                if (node.Evaluate())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
