using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class PlayerData
    {
        public int X, Y;

        public int PlayerLevelIndex;

        public int AttackBuffCount, VitalityBuffCount, MaxHealingQuartz, Keys;
    }
}
