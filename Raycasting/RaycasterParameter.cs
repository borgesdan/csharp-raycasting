using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Raycasting
{
    public class TextureRaycasterParameter : RaycasterParameter
    {
        public Texture2D Texture { get; set; }
        public int TextureWidth { get; set; }
        public int TextureHeight { get; set; }

        public TextureRaycasterParameter() { }

        public TextureRaycasterParameter(Texture2D texture, int width, int height, RaycasterParameter parameter)
            : base(parameter.Viewport, parameter.PlayerStartPosition, parameter.CameraInitialDirection, parameter.CameraPlane, parameter.WorldMap)
        {
            Texture = texture;
            TextureWidth = width;
            TextureHeight = height;
        }

        public TextureRaycasterParameter(Viewport viewport, Vector2 playerStartPosition,
           Vector2 cameraInitialDirection, Vector2 cameraPlane, int[,] map, Texture2D texture, int width, int height)
            : base(viewport, playerStartPosition, cameraInitialDirection, cameraPlane, map)
        {
            Texture = texture;
            TextureWidth = width;
            TextureHeight = height;
        }
    }

    public class ColorRaycasterParameter : RaycasterParameter
    {
        public Dictionary<int, Color> Colors { get; set; }
        public Game Game { get; set; }

        public ColorRaycasterParameter() { }

        public ColorRaycasterParameter(Dictionary<int, Color> colors, Game game, RaycasterParameter parameter)
            : base(parameter.Viewport, parameter.PlayerStartPosition, parameter.CameraInitialDirection, parameter.CameraPlane, parameter.WorldMap)
        {
            Colors = colors;
            Game = game;
        }

        public ColorRaycasterParameter(Viewport viewport, Vector2 playerStartPosition,
           Vector2 cameraInitialDirection, Vector2 cameraPlane, int[,] map, Dictionary<int, Color> colors, Game game)
            : base(viewport, playerStartPosition, cameraInitialDirection, cameraPlane, map)
        {
            Game = game;
            Colors = colors;
        }
    }

    public class RaycasterParameter
    {
        public Viewport Viewport { get; set; }
        public Vector2 PlayerStartPosition { get; set; }
        public Vector2 CameraInitialDirection { get; set; }
        public Vector2 CameraPlane { get; set; }
        public int[,] WorldMap { get; set; }

        public RaycasterParameter() { }

        public RaycasterParameter(Viewport viewport, Vector2 playerStartPosition,
            Vector2 cameraInitialDirection, Vector2 cameraPlane, int[,] map)
        {
            Viewport = viewport;
            PlayerStartPosition = playerStartPosition;
            CameraInitialDirection = cameraInitialDirection;
            CameraPlane = cameraPlane;
            WorldMap = map;
        }
    }
}
