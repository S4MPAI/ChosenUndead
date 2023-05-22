using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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

        private const string playerPath = "Entities/Player/";

        private const string sceletonPath = "Entities/Sceleton/";

        private const string bonfirePath = "Map/Decorations/bonfire";

        private const string tilesPath = "Map/Tiles/tile";

        private const string boardsPath = "Interface/Boards/";

        private const string forestBackgroundsPath = "Backgrounds/forest/bg_forest_";

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
            playerAnimations.AddAnimation(WeaponAttack.FirstAttack, new Animation(content.Load<Texture2D>($"{playerPath}FirstAttack"), 7, 0.125f, false));
            playerAnimations.AddAnimation(WeaponAttack.SecondAttack, new Animation(content.Load<Texture2D>($"{playerPath}SecondAttack"), 3, 0.125f, false));
            playerAnimations.AddAnimation(WeaponAttack.ThirdAttack, new Animation(content.Load<Texture2D>($"{playerPath}ThirdAttack"), 4, 0.125f, false));
            playerAnimations.AddAnimation(WeaponAttack.FourthAttack, new Animation(content.Load<Texture2D>($"{playerPath}FourthAttack"), 6, 0.125f, false));

            return playerAnimations;
        }

        public static AnimationManager<object> GetSceletonAnimations()
        {
            var sceletonAnimations = new AnimationManager<object>();

            sceletonAnimations.AddAnimation(EntityAction.Idle, new Animation(content.Load<Texture2D>($"{sceletonPath}Idle"), 4, 0.2f));
            sceletonAnimations.AddAnimation(EntityAction.Death, new Animation(content.Load<Texture2D>($"{sceletonPath}Death"), 4, 0.8f, false));
            //playerAnimations.AddAnimation(EntityAction.Run, new Animation(content.Load<Texture2D>($"{playerPath}Run"), 128, 64, 8, 0.125f));
            //playerAnimations.AddAnimation(EntityAction.Jump, new Animation(content.Load<Texture2D>($"{playerPath}Jump"), 128, 64, 8, 0.125f));
            //playerAnimations.AddAnimation(WeaponAttack.FirstAttack, new Animation(content.Load<Texture2D>($"{playerPath}FirstAttack"), 128, 64, 7, 0.125f));
            //playerAnimations.AddAnimation(WeaponAttack.SecondAttack, new Animation(content.Load<Texture2D>($"{playerPath}SecondAttack"), 128, 64, 3, 0.125f));
            //playerAnimations.AddAnimation(WeaponAttack.ThirdAttack, new Animation(content.Load<Texture2D>($"{playerPath}ThirdAttack"), 128, 64, 4, 0.125f));
            //playerAnimations.AddAnimation(WeaponAttack.FourthAttack, new Animation(content.Load<Texture2D>($"{playerPath}FourthAttack"), 128, 64, 6, 0.125f));

            return sceletonAnimations;
        }

        public static List<ScrollingBackground> GetForestBackgrounds(Point windowSize) => 
            new()
            {
                new(content.Load<Texture2D>(forestBackgroundsPath + 1), 0.0325f, windowSize),
                new(content.Load<Texture2D>(forestBackgroundsPath + 2), 0.065f, windowSize),
                new(content.Load<Texture2D>(forestBackgroundsPath + 3), 0.13f, windowSize)
            };

        public static Animation GetBonfireSaveAnimation() => new Animation(content.Load<Texture2D>(bonfirePath), 8, 0.1f);

        public static Texture2D GetTileTexture(int tileNumber) => tileNumber != 0 ? content.Load<Texture2D>(tilesPath + tileNumber) : null;

        public static Board GetBoardForLevelTransition() =>
            new Board(content.Load<Texture2D>(boardsPath + "LevelTransition"),  Color.White, Color.Black, "Покинуть локацию");

        public static Board GetBoardForBonfireSave() =>
            new Board(content.Load<Texture2D>(boardsPath +"Bonfire"), Color.White, Color.Black, "Cохраниться?");
    }

    public class Board : Sprite
    {
        public string Text { get; private set; }

        private SpriteFont font;

        private Color boardColor;

        private Color textColor;

        public Board(Texture2D texture, Color boardColor, Color textColor, string text = "",  SpriteFont spriteFont = null) : base(texture)
        {
            Text = text;
            font = spriteFont ?? Content.Load<SpriteFont>("Fonts/BoardFont");
            this.boardColor = boardColor;
            this.textColor = textColor;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, boardColor);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = Rectangle.X + Rectangle.Width / 2 - font.MeasureString(Text).X / 2;
                var y = Rectangle.Y + Rectangle.Height / 2 - font.MeasureString(Text).Y / 2;
                spriteBatch.DrawString(font, Text, new Vector2(x, y), textColor);
            }
        }

        public override void Update(GameTime gameTime)
        {
        }

        public void ChangeText(string text)
        {
            this.Text = text ?? this.Text;
        }
    }
}
