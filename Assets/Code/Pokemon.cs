using UnityEngine;

public class Pokemon
{
    public string name { get; private set; }
    public int id { get; private set; }
    public Sprite image { get; private set; }

    public Pokemon(string name, int id, Sprite image)
    {
        this.name = name;
        this.id = id;
        this.image = image;
    }
}
