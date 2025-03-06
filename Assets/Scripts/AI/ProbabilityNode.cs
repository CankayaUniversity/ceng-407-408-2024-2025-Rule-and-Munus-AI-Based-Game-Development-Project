using System;
using UnityEngine;

namespace BehaviorTree
{
    public class ProbabilityNode : Node
    {
        private float probability;
        private Node child;

        public ProbabilityNode(float probability, Node child)
        {
            this.probability = probability;
            this.child = child;
        }

        public override bool Evaluate()
        {
            return UnityEngine.Random.value < probability && child.Evaluate();
        }
    }
}
