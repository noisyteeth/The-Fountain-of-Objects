﻿
namespace Fountain;

public interface IEntity
{
    public NewCoord Position { get; set; }
    public bool Dead { get; set; }

    public NewCoord MoveEntity(NewCoord position);
}
