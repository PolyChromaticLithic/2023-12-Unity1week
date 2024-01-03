using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    private int id;
    public int ID
    {
        get
        {
            return id;
        }
        set
        {
            if (id != value)
            {
                id = value;
                icon = ResourceData.sprites[id];
            }
        }
    }
    private Sprite icon;
    public Sprite Icon
    {
        get
        {
            return icon;
        }
    }
    public string Name
    {
        get
        {
            switch (id)
            {
                case 0:
                    return "空";
                case 1:
                    return "奇妙なプレゼント";
                case 2:
                    return "クリスマスクッキー";
                case 3:
                    return "サンドイッチ";
                case 4:
                    return "ニンジン";
                default:
                    return "不明";
            }
        }
    }
    public string Description
    {
        get
        {
            switch (id)
            {
                case 0:
                    return "鞄はまだ空いている。";
                case 1:
                    return "家の前に置かれた、奇妙なプレゼント。不思議な機械が入っている。";
                case 2:
                    return "甘くてスパイシーなクッキー。体力を14回復する。";
                case 3:
                    return "ライ麦パンのトーストにチキンのスライスとチーズを挟んだサンドイッチ。\n噛みしめるごとに旨みがにじみだす。体力を152回復する。";
                case 4:
                    return "収穫したてのニンジン。みずみずしさを保っている。体力を21回復する。";
                default:
                    return "こんなのあったっけ……？\n(このアイテムが見えるのはバグなので開発者に連絡してください)";
            }
        }
    }
    public int amount;
    public bool IsUsable
    {
        get
        {
            switch (id)
            {
                case 0:
                    return false;
                case 1:
                    return false;
                case 2:
                    return true;
                case 3:
                    return true;
                case 4:
                    return true;
                default:
                    return false;
            }
        }
    }

    public bool IsDiscardable
    {
        get
        {
            switch (id)
            {
                case 0:
                    return false;
                case 1:
                    return false;
                case 2:
                    return true;
                case 3:
                    return true;
                case 4:
                    return true;
                default:
                    return false;
            }
        }
    }

    public List<Action> UseAction = new List<Action>
    {
        //0
        () =>
        {
            Player.instance.HP += 0;
        },
        //1
        () =>
        {
            Player.instance.HP += 0;
        },
        //2
        () =>
        {
            Player.instance.HP += 14;
        },
        //3
        () =>
        {
            Player.instance.HP += 152;
        },
        //4
        () =>
        {
            Player.instance.HP += 21;
        },
        //5
        () =>
        {
            Player.instance.HP += 0;
        },
        //6
        () =>
        {
            Player.instance.HP += 0;
        },
        //7
        () =>
        {
            Player.instance.HP += 0;
        },
        //8
        () =>
        {
            Player.instance.HP += 0;
        },

    };

    static public ItemData Empty
    {
        get
        {
            return new ItemData(0, 0);
        }
    }

    public ItemData(int id, int count)
    {
        this.id = id;
        this.amount = count;
        icon = ResourceData.sprites[id];
    }
}
