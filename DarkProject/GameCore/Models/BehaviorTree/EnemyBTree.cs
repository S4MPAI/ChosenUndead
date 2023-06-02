using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ChosenUndead
{
    public abstract class EnemyBTree : Enemy
    {
        public EnemyBTree(Map map, AnimationManager<object> animationManager, int hitBoxWidth, Weapon weapon, int attackWidth = 0, Entity target = null) : 
            base(map, animationManager, hitBoxWidth, weapon, attackWidth, target)
        {
        }

        private Node Root { get; set; }

        protected void Start()
        {
            Root = SetupTree();
        }

        public override void Update()
        {
            if (Root != null)
                Root.Evaluate();
        }

        protected abstract Node SetupTree();

        public override void SetStartPosition(Vector2 pos)
        {
            base.SetStartPosition(pos);

            Start();
        }
    }
}
