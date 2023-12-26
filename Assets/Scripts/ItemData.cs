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
                    return "��";
                case 1:
                    return "��ȃv���[���g";
                case 2:
                    return "�N���X�}�X�N�b�L�[";
                case 3:
                    return "�T���h�C�b�`";
                case 4:
                    return "�j���W��";
                default:
                    return "�s��";
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
                    return "���͂܂��󂢂Ă���B";
                case 1:
                    return "�Ƃ̑O�ɒu���ꂽ�A��ȃv���[���g�B�s�v�c�ȋ@�B�������Ă���B";
                case 2:
                    return "���ɂ��݂�قǊÂ��āA�s���b�ƃX�p�C�V�[�ȃN�b�L�[�B�̗͂�14�񕜂���B";
                case 3:
                    return "���C���p���̃g�[�X�g�Ƀ`�L���̃X���C�X�ƃ`�[�Y�����񂾃T���h�C�b�`�B\n���݂��߂邲�ƂɎ|�݂����ӂ��B�̗͂�152�񕜂���B";
                case 4:
                    return "���n�����Ẵj���W���B�݂��݂�������ۂ��Ă���B�̗͂�21�񕜂���B";
                default:
                    return "����Ȃ̂����������c�c�H\n(���̃A�C�e����������̂̓o�O�Ȃ̂ŊJ���҂ɘA�����Ă�������)";
            }
        }
    }
    public int amount;

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
