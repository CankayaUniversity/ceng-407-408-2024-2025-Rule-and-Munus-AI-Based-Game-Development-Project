using System;

namespace BehaviorTree
{
    public class ActionNode : Node
    {
        private Action nodeAction; // 'action' yerine 'nodeAction' kullan�yoruz.

        public ActionNode(Action action)
        {
            this.nodeAction = action; // De�i�ken ad� �ak��mas�n diye farkl� bir isim kulland�k.
        }

        public override bool Evaluate()
        {
            nodeAction?.Invoke(); // `nodeAction` �a��rarak hatay� ��z�yoruz.
            return true;
        }
    }
}
