using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class Node
    {
        public static uint LastId = 0;

        protected NodeState state;
        public NodeState State { get => state; }

        private Node root;
        protected Node Root => root;

        private Node parent;
        protected List<Node> children = new List<Node>();
        private Dictionary<string, object> dataContext = new Dictionary<string, object>();
        public Dictionary<string, object> Data => dataContext;
        //protected Dictionary<string, System.Action> callbacks;
        //public Dictionary<string, System.Action> Callbacks => callbacks;

        public Node Parent => parent;
        public List<Node> Children => children;
        public bool HasChildren => children.Count > 0;

        public Node()
        {
            parent = null;
            root = this;
        }
        public Node(List<Node> children) : this()
        {
            SetChildren(children);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetChildren(List<Node> children, bool forceRoot = false)
        {
            foreach (Node c in children)
                Attach(c);
            if (forceRoot)
                SetRoot(this);
        }

        public void Attach(Node child)
        {
            children.Add(child);
            child.parent = this;
            child.root = root;
        }

        public void Detach(Node child)
        {
            children.Remove(child);
            child.parent = null;
            child.root = null;
        }

        public void SetRoot(Node root)
        {
            this.root = root;
            foreach (Node child in children)
                child.SetRoot(root);
        }

        public object GetData(string key)
        {
            object value = null;
            if (dataContext.TryGetValue(key, out value))
                return value;

            Node node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            if (dataContext.ContainsKey(key))
            {
                dataContext.Remove(key);
                return true;
            }

            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }

        public void SetData(string key, object value)
        {
            dataContext[key] = value;
        }

        public void SetDataOnMainElement(string key, object value)
        {
            var mainElement = this;

            while(mainElement.parent != null)
                mainElement = mainElement.parent;

            mainElement.SetData(key, value);
        }

        //public void SetCallbacks(Dictionary<string, System.Action> callbacks)
        //{
        //    this.callbacks = callbacks;
        //}

        //public void AddCallbacks(Dictionary<string, System.Action> callbacks)
        //{
        //    if (this.callbacks == null)
        //        this.callbacks = new Dictionary<string, Action>();
        //    foreach (var p in callbacks) this.callbacks.Add(p.Key, p.Value);
        //}
    }
}
