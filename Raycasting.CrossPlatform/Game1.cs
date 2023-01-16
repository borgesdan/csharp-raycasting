using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Raycasting.CrossPlatform
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        ColorRaycaster colorRaycaster;
        TextureRaycaster textureRaycaster;

        Viewport colorViewport = new Viewport(0, 0, 640, 480);
        Viewport textureViewport = new Viewport(640, 0, 640, 480);

        int[,] worldMap = new int[24, 24]
        {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,2,2,2,2,2,0,0,0,0,3,0,3,0,3,0,0,0,1},
            {1,0,0,0,0,0,2,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,2,0,0,0,2,0,0,0,0,3,0,0,0,3,0,0,0,1},
            {1,0,0,0,0,0,2,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,2,2,0,2,2,0,0,0,0,3,0,3,0,3,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,3,3,0,3,0,3,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,1},
            {1,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,3,3,3,0,3,0,3,0,0,0,0,0,0,0,0,2,2,2,2,0,0,1},
            {1,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,1},
            {1,0,0,0,3,3,0,3,0,3,0,0,0,0,0,0,0,0,0,0,2,0,0,1},
            {1,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,2,0,0,1},
            {1,4,0,4,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,2,5,2,1},
            {1,4,0,0,0,0,5,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,4,0,4,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,4,0,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
        };

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 480;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            colorRaycaster = new ColorRaycaster(new ColorRaycasterParameter
            {
                Colors = new Dictionary<int, Color>()
                {                    
                    { 1, Color.Yellow },
                    { 2, Color.Green },
                    { 3, Color.Blue },
                    { 4, Color.Yellow },
                    { 5, Color.White },
                },
                Game = this,
                CameraInitialDirection = new Vector2(-1F, 0F),
                CameraPlane = new Vector2(0, 0.66F),
                PlayerStartPosition = new Vector2(22F, 12F),
                Viewport = colorViewport,
                WorldMap = worldMap,
            });

            textureRaycaster = new TextureRaycaster(new TextureRaycasterParameter
            {
                Texture = Content.Load<Texture2D>("textures/wolftextures"),
                TextureWidth = 64,
                TextureHeight = 64,
                CameraInitialDirection = new Vector2(-1F, 0F),
                CameraPlane = new Vector2(0, 0.66F),
                PlayerStartPosition = new Vector2(22F, 12F),
                Viewport = textureViewport,
                WorldMap = worldMap,
            }) ;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here            

            var keyboard = Keyboard.GetState();
            float moveSpeed = 0.05F;
            float rotation = 0.03F;

            if (keyboard.IsKeyDown(Keys.Up))
            {
                colorRaycaster.MoveForward(moveSpeed);
                textureRaycaster.MoveForward(moveSpeed);
            }
            else if (keyboard.IsKeyDown(Keys.Down))
            {
                colorRaycaster.MoveBack(moveSpeed);
                textureRaycaster.MoveBack(moveSpeed);
            }

            if (keyboard.IsKeyDown(Keys.Right))
            {
                colorRaycaster.MoveRight(rotation);
                textureRaycaster.MoveRight(rotation);
            }
            else if (keyboard.IsKeyDown(Keys.Left))
            {
                colorRaycaster.MoveLeft(rotation);
                textureRaycaster.MoveLeft(rotation);
            }

            colorRaycaster.Update(gameTime);
            textureRaycaster.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here

            GraphicsDevice.Viewport = colorViewport;
            
            _spriteBatch.Begin();
            colorRaycaster.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

            GraphicsDevice.Viewport = textureViewport;

            _spriteBatch.Begin();
            textureRaycaster.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

            Window.Title = $"Camera Position{textureRaycaster.Camera}";

            base.Draw(gameTime);
        }
    }
}