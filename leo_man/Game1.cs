using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace leo_man
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _playerTexture;   
        private Vector2 _playerPosition;   
        private float _speed = 200f;        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Resolución de la ventana
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 480;
        }

        protected override void Initialize()
        {
            // Posición inicial en el centro de la pantalla
            _playerPosition = new Vector2(
                _graphics.PreferredBackBufferWidth / 2f,
                _graphics.PreferredBackBufferHeight / 2f
            );

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Cargar la imagen desde la carpeta Content
            // Escribe el nombre EXACTO del archivo sin la extensión
            // Ejemplo: si es pixil-frame-0.png -> "pixil-frame-0"
            _playerTexture = Content.Load<Texture2D>("pixil-frame-0");
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Escape))
                Exit();

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Movimiento con WASD
            if (keyboard.IsKeyDown(Keys.W))
                _playerPosition.Y -= _speed * delta;
            if (keyboard.IsKeyDown(Keys.S))
                _playerPosition.Y += _speed * delta;
            if (keyboard.IsKeyDown(Keys.A))
                _playerPosition.X -= _speed * delta;
            if (keyboard.IsKeyDown(Keys.D))
                _playerPosition.X += _speed * delta;

            // Limitar el movimiento dentro de la ventana
            _playerPosition.X = MathHelper.Clamp(_playerPosition.X, 0, _graphics.PreferredBackBufferWidth - _playerTexture.Width);
            _playerPosition.Y = MathHelper.Clamp(_playerPosition.Y, 0, _graphics.PreferredBackBufferHeight - _playerTexture.Height);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_playerTexture, _playerPosition, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
