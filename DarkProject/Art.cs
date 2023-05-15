using ChosenUndead.GameCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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

        public static void Initialize(ContentManager content)
        {
            Art.content = content;
        }

        public static AnimationManager<object> GetPlayerAnimations()
        {
            var playerAnimations = new AnimationManager<object>();

            playerAnimations.AddAnimation(EntityAction.Idle, new Animation(content.Load<Texture2D>($"{playerPath}Idle"), 128, 64, 8, 0.125f));
            playerAnimations.AddAnimation(EntityAction.Run, new Animation(content.Load<Texture2D>($"{playerPath}Run"), 128, 64, 8, 0.125f));
            playerAnimations.AddAnimation(EntityAction.Jump, new Animation(content.Load<Texture2D>($"{playerPath}Jump"), 128, 64, 8, 0.125f));
            playerAnimations.AddAnimation(WeaponAttack.FirstAttack, new Animation(content.Load<Texture2D>($"{playerPath}FirstAttack"), 128, 64, 7, 0.125f));
            playerAnimations.AddAnimation(WeaponAttack.SecondAttack, new Animation(content.Load<Texture2D>($"{playerPath}SecondAttack"), 128, 64, 3, 0.125f));
            playerAnimations.AddAnimation(WeaponAttack.ThirdAttack, new Animation(content.Load<Texture2D>($"{playerPath}ThirdAttack"), 128, 64, 4, 0.125f));
            playerAnimations.AddAnimation(WeaponAttack.FourthAttack, new Animation(content.Load<Texture2D>($"{playerPath}FourthAttack"), 128, 64, 6, 0.125f));

            return playerAnimations;
        }

        public static AnimationManager<object> GetSceletonAnimations()
        {
            var sceletonAnimations = new AnimationManager<object>();

            sceletonAnimations.AddAnimation(EntityAction.Idle, new Animation(content.Load<Texture2D>($"{sceletonPath}Idle"), 150, 64, 4, 0.2f));
            sceletonAnimations.AddAnimation(EntityAction.Death, new Animation(content.Load<Texture2D>($"{sceletonPath}Death"), 150, 64, 4, 0.8f));
            //playerAnimations.AddAnimation(EntityAction.Run, new Animation(content.Load<Texture2D>($"{playerPath}Run"), 128, 64, 8, 0.125f));
            //playerAnimations.AddAnimation(EntityAction.Jump, new Animation(content.Load<Texture2D>($"{playerPath}Jump"), 128, 64, 8, 0.125f));
            //playerAnimations.AddAnimation(WeaponAttack.FirstAttack, new Animation(content.Load<Texture2D>($"{playerPath}FirstAttack"), 128, 64, 7, 0.125f));
            //playerAnimations.AddAnimation(WeaponAttack.SecondAttack, new Animation(content.Load<Texture2D>($"{playerPath}SecondAttack"), 128, 64, 3, 0.125f));
            //playerAnimations.AddAnimation(WeaponAttack.ThirdAttack, new Animation(content.Load<Texture2D>($"{playerPath}ThirdAttack"), 128, 64, 4, 0.125f));
            //playerAnimations.AddAnimation(WeaponAttack.FourthAttack, new Animation(content.Load<Texture2D>($"{playerPath}FourthAttack"), 128, 64, 6, 0.125f));

            return sceletonAnimations;
        }

        public static Animation GetBonfireSaveAnimation() => new Animation(content.Load<Texture2D>(bonfirePath), 24, 32, 8, 0.1f);

        public static Texture2D GetTileTexture(int tileNumber) => content.Load<Texture2D>(tilesPath + tileNumber);

        public static Board GetBoardForLevelTransition() =>
            new Board(content.Load<Texture2D>(boardsPath + "LevelTransition"),  Color.White, Color.Black, "Покинуть локацию");

        public static Board GetBoardForBonfireSave() =>
            new Board(content.Load<Texture2D>(boardsPath +"Bonfire"), Color.White, Color.Black, "Cохраниться?");
    }

    public class Board : Component
    {
        public string Text { get; private set; }

        private SpriteFont font;

        private Color boardColor;

        private Color textColor;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            }
        }

        public Board(Texture2D texture, Color boardColor, Color textColor, string text = "",  SpriteFont spriteFont = null)
        {
            this.texture = texture;
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
