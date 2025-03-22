using System.Collections.Generic;

namespace BehaviorTree
{
    public class Sequence : Node
    {
        private List<Node> nodes;

        public Sequence(List<Node> nodes)
        {
            this.nodes = nodes;
        }

        public override bool Evaluate()
        {
            foreach (Node node in nodes)
            {
                if (!node.Evaluate())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
