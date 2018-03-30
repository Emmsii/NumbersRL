using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NumbersRL.Input;

namespace NumbersRL
{

    public class MainGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private DelayedInputManager _delayedInputManager;

        private Font font;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _delayedInputManager = new DelayedInputManager();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            string chars = "0123456789ABCDEF" +
                          "GHIJKLMNOPQRSTUV" +
                          "WXYZ????????????" +
                          "?????abcdefghijk" +
                          "lmnopqrstuvwxyz?" +
                          "????????????????" +
                          "#%&@$.,!?:;'\"()[" +
                          "]*/\\+-<=>???????" +
                          "???⚪◔◑◕⚫█▓▒░??? ";

            font = new Font(Content.Load<Texture2D>("font"), 8, 16, chars);

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }


        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _delayedInputManager.Update(Keyboard.GetState());

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            font.DrawString("+2", 20, 10, 2, Color.Green, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
