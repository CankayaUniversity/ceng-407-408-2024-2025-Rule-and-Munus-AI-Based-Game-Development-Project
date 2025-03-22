using System;

namespace BehaviorTree
{
    public class ActionNode : Node
    {
        private Action nodeAction; // 'action' yerine 'nodeAction' kullanýyoruz.

        public ActionNode(Action action)
        {
            this.nodeAction = action; // Deðiþken adý çakýþmasýn diye farklý bir isim kullandýk.
        }

        public override bool Evaluate()
        {
            nodeAction?.Invoke(); // `nodeAction` çaðýrarak hatayý çözüyoruz.
            return true;
        }
    }
}
