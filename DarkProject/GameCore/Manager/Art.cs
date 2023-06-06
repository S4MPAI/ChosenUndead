using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public static class Art
    {
        private static ContentManager content;
        private static Map map;
        private const string playerPath = "Entities/Player/";
        private const string sceletonPath = "Entities/Sceleton/";
        private const string goblinPath = "Entities/Goblin/";
        private const string bonfirePath = "Map/Decorations/bonfire";
        private const string tilesPath = "Map/Tiles/tile";
        private const string boardsPath = "Interface/Boards/";
        private const string forestBackgroundsPath = "Backgrounds/forest/bg_forest_";
        private const string chestsPath = "Map/Decorations/Chests/chest";
        private const string npcPath = "Entities/Npc/";
        private const string healthBarPath = "Interface/HealthBar/";
        private const string videoPath = "Videos/";

        public static void Initialize(ContentManager content)
        {
            Art.content = content;
        }

        public static AnimationManager<object> GetPlayerAnimations()
        {
            var playerAnimations = new AnimationManager<object>();
            
            playerAnimations.AddAnimation(EntityAction.Idle, new Animation(content.Load<Texture2D>($"{playerPath}Idle"), 8, 0.125f));
            playerAnimations.AddAnimation(EntityAction.Run, new Animation(content.Load<Texture2D>($"{playerPath}Run"), 8, 0.125f));
            playerAnimations.AddAnimation(EntityAction.Jump, new Animation(content.Load<Texture2D>($"{playerPath}Jump"), 8, 0.125f));
            playerAnimations.AddAnimation(Attacks.FirstAttack, new Animation(content.Load<Texture2D>($"{playerPath}FirstAttack"), 7, 0.125f, false));
            playerAnimations.AddAnimation(Attacks.SecondAttack, new Animation(content.Load<Texture2D>($"{playerPath}SecondAttack"), 3, 0.125f, false));
            playerAnimations.AddAnimation(Attacks.ThirdAttack, new Animation(content.Load<Texture2D>($"{playerPath}ThirdAttack"), 4, 0.125f, false));
            playerAnimations.AddAnimation(Attacks.FourthAttack, new Animation(content.Load<Texture2D>($"{playerPath}FourthAttack"), 6, 0.125f, false));
            playerAnimations.AddAnimation(EntityAction.Healing, new Animation(content.Load<Texture2D>($"{playerPath}Healing"), 8, 0.5f, false));
            playerAnimations.AddAnimation(EntityAction.Roll, new Animation(content.Load<Texture2D>($"{playerPath}Roll"), 4, 0.15f));
            playerAnimations.AddAnimation(EntityAction.Death, new Animation(content.Load<Texture2D>($"{playerPath}Death"), 4, 0.2f, false));
            playerAnimations.AddAnimation(EntityAction.Hurt, new Animation(content.Load<Texture2D>($"{playerPath}Hurt"), 3, 0.4f, false));

            return playerAnimations;
        }

        public static AnimationManager<object> GetSceletonAnimations()
        {
            var sceletonAnimations = new AnimationManager<object>();

            sceletonAnimations.AddAnimation(EntityAction.Idle, new Animation(content.Load<Texture2D>($"{sceletonPath}Idle"), 4, 0.2f));
            sceletonAnimations.AddAnimation(EntityAction.Death, new Animation(content.Load<Texture2D>($"{sceletonPath}Death"), 4, 0.8f, false));
            sceletonAnimations.AddAnimation(EntityAction.Run, new Animation(content.Load<Texture2D>($"{sceletonPath}Walk"), 4, 0.15f));
            sceletonAnimations.AddAnimation(Attacks.FirstAttack, new Animation(content.Load<Texture2D>($"{sceletonPath}Attack"), 8, 0.125f));

            return sceletonAnimations;
        }

        public static AnimationManager<object> GetGoblinAnimations()
        {
            var goblinAnimations = new AnimationManager<object>();

            goblinAnimations.AddAnimation(EntityAction.Idle, new Animation(content.Load<Texture2D>($"{goblinPath}Idle"), 4, 0.2f));
            goblinAnimations.AddAnimation(EntityAction.Run, new Animation(content.Load<Texture2D>($"{goblinPath}Run"), 8, 0.2f));
            goblinAnimations.AddAnimation(Attacks.FirstAttack, new Animation(content.Load<Texture2D>($"{goblinPath}Attack"), 8, 1f));
            goblinAnimations.AddAnimation(EntityAction.Death, new Animation(content.Load<Texture2D>($"{goblinPath}Death"), 4, 0.2f, false));

            return goblinAnimations;
        }

        public static (Texture2D bar, Texture2D progressBar, Texture2D progressBarBorder) GetBars() =>
            (
                content.Load<Texture2D>($"{healthBarPath}Bar"),
                content.Load<Texture2D>($"{healthBarPath}ProgressBar"),
                content.Load<Texture2D>($"{healthBarPath}ProgressBarBorder")
            );

        public static AnimationManager<object> GetNpcAnimations(string name)
        {
            var npcAnimations = new AnimationManager<object>();

            var framesCount = 0;
            if (name == "DirtMan")
                framesCount = 5;
            if (name == "Archer")
                framesCount = 4;

            npcAnimations.AddAnimation(EntityAction.Idle, new Animation(content.Load<Texture2D>($"{npcPath}{name}/Idle"), framesCount, 0.2f));

            return npcAnimations;
        }

        public static List<ScrollingBackground> GetForestBackgrounds(Point windowSize) =>
            new()
            {
                new(content.Load<Texture2D>(forestBackgroundsPath + 1), 0.0325f, windowSize),
                new(content.Load<Texture2D>(forestBackgroundsPath + 2), 0.065f, windowSize),
                new(content.Load<Texture2D>(forestBackgroundsPath + 3), 0.13f, windowSize)
            };

        public static Animation GetBonfireSaveAnimation() => new Animation(content.Load<Texture2D>(bonfirePath), 8, 0.1f);

        public static Animation GetChestAnimation(ChestItem chestType) => new Animation(content.Load<Texture2D>($"{chestsPath}{chestType}"), 7, 0.2f, false);

        public static Texture2D GetTileTexture(int tileNumber) => tileNumber != 0 ? content.Load<Texture2D>(tilesPath + tileNumber) : null;

        public static Board GetBoardForLevelTransition() =>
            new Board(content.Load<Texture2D>(boardsPath + "LevelTransition"), Color.White, Color.Black, "Покинуть локацию");

        public static Board GetBoardForBonfireSave() =>
            new Board(content.Load<Texture2D>(boardsPath + "Bonfire"), Color.White, Color.Black, "Cохраниться?");

        public static Board GetBoardForNpc() =>
            new Board(content.Load<Texture2D>(boardsPath + "NPC"), Color.White, Color.Black);

        public static void SetPositionInMapBounds(Component component)
        {
            component.Position.X = MathHelper.Clamp(component.Position.X, 0, map.MapSize.X);
            component.Position.Y = MathHelper.Clamp(component.Position.Y, 0, map.MapSize.Y);
        }

        public static void SetMap(Map map) => Art.map = map;

        public static Board GetBoardForChest(ChestItem item)
        {
            var text = "Вы получили ";

            switch (item)
            {
                case ChestItem.Attack:
                    text += "силу";
                    break;
                case ChestItem.Vitality:
                    text += "здоровье";
                    break;
                case ChestItem.HealingQuartz:
                    text += "\nлечащий камень";
                    break;
                case ChestItem.Key:
                    text += "ключ";
                    break;
            }

            return new Board(content.Load<Texture2D>(boardsPath + "Chest"), Color.White, Color.Black, text);
        }




        public static SpriteFont GetFont(string name) =>
            content.Load<SpriteFont>($"Fonts/{name}");

        public static Button GetButton(string text, Vector2 position)
        {
            var buttonTexture = content.Load<Texture2D>("Controls/menuButton");
            var buttonFont = Art.GetFont("Font");

            return new Button(buttonTexture, buttonFont)
            {
                Text = text,
                Position = position
            };
        }

        public static Button GetButton(string text) => GetButton(text, Vector2.Zero);

        public static Video GetVideo(string name) => content.Load<Video>($"{videoPath}{name}");
    }

    public class Board : Sprite
    {
        public string Text { get; private set; }

        private SpriteFont font;

        private Color boardColor;

        private Color textColor;

        public Board(Texture2D texture, Color boardColor, Color textColor, string text = "", SpriteFont spriteFont = null) : base(texture)
        {
            Text = text;
            font = spriteFont ?? Art.GetFont("BoardFont");
            this.boardColor = boardColor;
            this.textColor = textColor;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, boardColor);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = Rectangle.X + Rectangle.Width / 2 - font.MeasureString(Text).X / 2;
                var y = Rectangle.Y + Rectangle.Height / 2 - font.MeasureString(Text).Y / 2;
                spriteBatch.DrawString(font, Text, new Vector2(x, y), textColor);
            }
        }

        public override void Update()
        {
        }

        public void ChangeText(string text)
        {
            Text = text ?? Text;
        }
    }
}
