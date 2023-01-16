using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Raycasting
{
    public class TextureRaycaster : Raycaster
    {
        int textureWidth = 0;
        int textureHeight = 0;
        Texture2D texture;
        int[] TextureIndices;
        int[] TextureXPositions;
        float[] TextureHeights;

        public TextureRaycaster(TextureRaycasterParameter parameter) :
            base(parameter)
        {
            TextureIndices = new int[parameter.Viewport.Width];
            TextureXPositions = new int[parameter.Viewport.Width];
            TextureHeights = new float[parameter.Viewport.Width];
            textureWidth = parameter.TextureWidth; 
            textureHeight = parameter.TextureHeight;
            texture = parameter.Texture;

            OnEndForUpdate += TextureRaycaster_OnEndForUpdate;
        }

        private void TextureRaycaster_OnEndForUpdate(int x, RaycasterUpdateResult obj)
        {
            var point = obj.Point;
            var side = obj.Side;
            var perpendicularWallDistance = obj.PerpendicularWallDistance;
            var rayDirection = obj.RayDirection;
            var lineHeight = obj.LineHeight;
            var drawStart = obj.DrawStart;

            var textureNumber = worldMap[point.X, point.Y] - 1;

            float wallX;

            if (side == 0)
                wallX = position.Y + perpendicularWallDistance * rayDirection.Y;
            else
                wallX = position.X + perpendicularWallDistance * rayDirection.X;

            wallX -= (float)Math.Floor(wallX);

            int textureX = (int)(wallX * textureWidth);

            if (side == 0 && rayDirection.X > 0 || side == 1 && rayDirection.Y < 0)
                textureX = textureWidth - textureX - 1;

            float step = 1F * textureHeight / lineHeight;
            float texturePosition = (drawStart - viewport.Height / 2 + lineHeight / 2) * step;

            TextureIndices[x] = textureNumber;
            TextureXPositions[x] = textureX;
            TextureHeights[x] = texturePosition;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int x = 0; x < viewport.Width; x++)
            {
                var line = Results[x].Line;

                float step = 1F * textureHeight / line.LineHeight;
                float texturePosition = (line.Start - viewport.Height / 2 + line.LineHeight / 2) * step;

                Rectangle destination;
                destination.X = x;
                destination.Y = line.Start;
                destination.Width = 1;
                destination.Height = line.End;

                Rectangle source;
                source.X = (TextureIndices[x] * textureWidth) + TextureXPositions[x];
                source.Y = 0;
                source.Width = 1;
                source.Height = textureHeight;

                var side = Results[x].Side;
                var color = Color.White;

                if (side == 0)
                    color = Color.Lerp(Color.Gray, Color.White, 0.5F);

                spriteBatch.Draw(texture, destination, source, color);
            }
            
        }
    }
}
