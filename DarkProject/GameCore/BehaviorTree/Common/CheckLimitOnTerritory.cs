using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChosenUndead
{
    public class CheckLimitOnTerritory : Node
    {
        private Map map;

        private Enemy enemy;

        public CheckLimitOnTerritory(Enemy enemy, Map map) : base()
        {
            this.map = map;
            this.enemy = enemy;
        }

        public override NodeState Evaluate()
        {
            var hitBox = enemy.HitBox;
            var velocityX = (float)(GetData("velocityX") ?? 0.0f) * Time.ElapsedSeconds;
            var tileY = (int)Math.Ceiling((float)hitBox.Bottom / map.TileSize);

            if (!map.IsHaveCollision((int)(hitBox.Left + velocityX) / map.TileSize, tileY) && velocityX < 0 ||
                !map.IsHaveCollision((int)(hitBox.Right + velocityX) / map.TileSize, tileY) && velocityX > 0)
            {
                SetDataOnMainElement("velocityX", 0.0f);
                return NodeState.FAILURE;
            }


            return NodeState.SUCCESS;

        }
    }
}
