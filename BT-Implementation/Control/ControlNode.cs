using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT_Implementation.Control
{
    //one to many
    public abstract class ControlNode : BTNode
    {
        protected List<BTNode> children = new List<BTNode>();

        protected ControlNode(string name) : base(name)
        {
        }

        public void AddChild(BTNode node)
        {
            children.Add(node);
        }
        // added in case of creating GUI For nodes
        public void RemoveChild(BTNode node) 
        { 
            
            children.Remove(node);
        }

    }
}
