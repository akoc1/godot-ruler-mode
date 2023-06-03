using Godot;
using System;

public partial class Main : Node2D
{
    Font font;
    float thickness = 2f;
    bool pointsVisible = true;
    Vector2 clickedPosition = Vector2.Zero;

    public override void _Ready()
    {
        font = ResourceLoader.Load<Font>("res://fonts/Ubuntu-Medium.ttf");
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("LMB"))
        {
            clickedPosition = GetGlobalMousePosition();
        }

        if (Input.IsActionJustPressed("F"))
        {
            pointsVisible = !pointsVisible;
        }

        QueueRedraw();
    }

    /*
    int calculateLength(Vector2 firstVector, Vector2 secondVector)
    {
        return (int)MathF.Sqrt(MathF.Pow(secondVector.X - firstVector.X, 2) + MathF.Pow(secondVector.Y - firstVector.Y, 2));
    }
    */

    public override void _Draw()
    {
        DrawString(font, new Vector2(16, GetViewportRect().Size.Y - 16), "F -> toggle points", HorizontalAlignment.Center, -1, 24, new Color("FFFFFF"));

        if (Input.IsActionPressed("LMB"))
        {
            //Fonts
            DrawString(font, new Vector2(0, 16), "Adjacent Side: " + (MathF.Abs(GetGlobalMousePosition().Y - clickedPosition.Y)).ToString(), HorizontalAlignment.Left, -1, 16, new Color("DD58D6"));
            DrawString(font, new Vector2(0, 40), "Adjacent Side: " + (MathF.Abs(GetGlobalMousePosition().X - clickedPosition.X)).ToString(), HorizontalAlignment.Left, -1, 16, new Color("73BBC9"));
            DrawString(font, new Vector2(0, 64), "Hypotenuse: " + (MathF.Sqrt(MathF.Pow(MathF.Abs(GetGlobalMousePosition().Y - clickedPosition.Y), 2) + MathF.Pow(MathF.Abs(GetGlobalMousePosition().X - clickedPosition.X), 2))).ToString(), HorizontalAlignment.Left, -1, 16, new Color("F2D8D8"));
            
            //Lines
            DrawLine(clickedPosition, GetGlobalMousePosition(), new Color("F2D8D8"), thickness, true); // Hypotenuse
            DrawLine(clickedPosition, new Vector2(clickedPosition.X, GetGlobalMousePosition().Y), new Color("DD58D6"), thickness, true); // left
            DrawLine(new Vector2(clickedPosition.X, GetGlobalMousePosition().Y), GetGlobalMousePosition(), new Color("73BBC9"), thickness, true); // bottom

            //Points
            if (pointsVisible)
            {
                DrawCircle(clickedPosition, thickness * 2, new Color("FFE569"));
                DrawCircle(GetGlobalMousePosition(), thickness * 2, new Color("FFE569"));
                DrawCircle(new Vector2(clickedPosition.X, GetGlobalMousePosition().Y), thickness * 2, new Color("FFE569"));
            }
        }
    }
}
