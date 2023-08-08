using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBox
{
    public float LeftX
		{
			get
			{
				return Math.Min(this.start.x, Mouse.GetMouse.GetMousePos.x);
			}
		} 
		public float RightX
		{
			get
			{
				return Math.Max(this.start.x,  Mouse.GetMouse.GetMousePos.x);
			}
		}
 
		public float BotY
		{
			get
			{
				return Math.Min(this.start.y,  Mouse.GetMouse.GetMousePos.y);
			}
		}
 
		public float TopY
		{
			get
			{
				return Math.Max(this.start.y,  Mouse.GetMouse.GetMousePos.y);
			}
		} 
		public Rect ScreenRect
		{
			get
			{
				
				Vector2 vector =  start.MapToUIPosition();
				Vector2 mousePositionOnUIInverted = UI.MousePositionOnUIInverted;
				if (mousePositionOnUIInverted.x < vector.x)
				{
					float x = mousePositionOnUIInverted.x;
					mousePositionOnUIInverted.x = vector.x;
					vector.x = x;
				}
				if (mousePositionOnUIInverted.y < vector.y)
				{
					float y = mousePositionOnUIInverted.y;
					mousePositionOnUIInverted.y = vector.y;
					vector.y = y;
				}
				return new Rect
				{
					xMin = vector.x,
					xMax = mousePositionOnUIInverted.x,
					yMin = vector.y,
					yMax = mousePositionOnUIInverted.y
				};
			}
		} 
		public bool IsValid
		{
			get
			{
				return (this.start - Mouse.GetMouse.GetMousePos).magnitude > 0.5f;
			}
		}
 
		public bool IsValidAndActive
		{
			get
			{
				return this.active && this.IsValid;
			}
		} 
		public void DragBoxOnGUI()
		{
			if (this.IsValidAndActive)
			{
				Rect rect = ScreenRect;
                GUI.Box(rect,"");
			}
		}
 
		// public bool Contains(Thing t)
		// {
		// 	if (t is Pawn)
		// 	{
		// 		return this.Contains((t as Pawn).Drawer.DrawPos);
		// 	}
		// 	foreach (IntVec3 intVec in t.OccupiedRect())
		// 	{
		// 		if (this.Contains(intVec.ToVector3Shifted()))
		// 		{
		// 			return true;
		// 		}
		// 	}
		// 	return false;
		// }
 
		public bool Contains(Vector3 v)
		{
			return v.x + 0.5f > this.LeftX && v.x - 0.5f < this.RightX && v.z + 0.5f > this.BotY && v.z - 0.5f < this.TopY;
		} 
		public bool active;
 
		public Vector3 start; 
		public bool startDrag;
		private const float DragBoxMinDiagonal = 0.5f;
}
