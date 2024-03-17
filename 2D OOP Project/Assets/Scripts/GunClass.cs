using System.Diagnostics.Tracing;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Rendering;

public class Weapons
{
    private string name;
    private int ammo;
    private int staticAmmo;
    private int mags;
    private Sprite sprite;
    private float fireRate;

    public void reload()
    {
        if(mags > 0)
        {
            mags--;
            ammo = staticAmmo;
        }
        
    }
    public string getName()
    {
        return this.name;
    }
    public void setName(string isim)
    {
        this.name = isim;
    }
    public int getAmmo()
    {
        return this.ammo;
    }
    public void setAmmo(int msayisi)
    {
        this.ammo = msayisi;
    }
    public int getStaticAmmo()
    {
        return this.staticAmmo;
    }
    public void setStaticAmmo()
    {
        this.staticAmmo = ammo;
    }
    public int getMags()
    {
        return this.mags;
    }
    public void setMags(int magsSayisi)
    {
        this.mags = magsSayisi;
    }
    public Sprite getSprite()
    {
        return this.sprite;
    }
    public void setSprite(Sprite sp)
    {
        this.sprite = sp;
    }
    public float getFireRate()
    {
        return this.fireRate;
    }
    public void setFireRate(float fr)
    {
        this.fireRate = fr;
    }
}

public class Pistols : Weapons
{
    public Pistols()
    {
        setFireRate(0.3f);
    }
}

public class Rifles : Weapons
{
    public Rifles()
    {
        setFireRate(0.18f);
    }
}

/*
---------------------------Guns---------------------------
*/

public class Deagle : Pistols
{
    public Deagle()
    {
        setName("Deagle");
        setAmmo(7);
        setStaticAmmo();
        setMags(9999);
        setSprite(Resources.Load<Sprite>("Sprites/Deagle"));
    }
}

public class Revolver : Pistols
{
    public Revolver()
    {
        setName("Revolver");
        setAmmo(6);
        setStaticAmmo();
        setMags(9999);
        setSprite(Resources.Load<Sprite>("Sprites/Revolver"));
    }
}

public class AK47 : Rifles
{
    public AK47()
    {
        setName("AK-47");
        setAmmo(30);
        setStaticAmmo();
        setMags(1);
        setSprite(Resources.Load<Sprite>("Sprites/AK47"));
    }
}

public class Scar : Rifles
{
    public Scar()
    {
        setName("SCAR");
        setAmmo(30);
        setStaticAmmo();
        setMags(1);
        setSprite(Resources.Load<Sprite>("Sprites/SCAR"));
    }
}
//
