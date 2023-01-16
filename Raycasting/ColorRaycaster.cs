using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Raycasting
{
    public class ColorRaycaster : Raycaster
    {
        Dictionary<int, Color> colors;
        Texture2D texture;

        public ColorRaycaster(ColorRaycasterParameter parameter) :
            base(parameter)
        {
            colors = parameter.Colors;
            texture = TextureHelper.GetFilledTexture(parameter.Game, 1, 1, Color.White);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int x = 0; x < viewport.Width; x++)
            {
                var result = Results[x];

                var contains = colors.TryGetValue(worldMap[result.Point.X, result.Point.Y], out Color color);

                if (!contains)
                    return;

                if (result.Side == 1)
                    color = Color.Lerp(Color.Gray, color, 0.5F);

                var line = result.Line;

                var destinationRectangle = new Rectangle(x, line.Start, 1, line.End);

                spriteBatch.Draw(texture, destinationRectangle, color);
            }
        }
    }
}
