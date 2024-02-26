using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class BloodMagicBullet_Cast : Base_SkillCast
{
    public override IEnumerator skillCast()
    {
        while (true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(Managers.Game.player.gameObject.transform.position, Camera.main.orthographicSize * 2 + 1.5f, LayerMask.GetMask("Monster"));
            if(colliders == null) { yield return null; }
            go = Managers.Game.objectPool.activateObject(typeof(Base_SkillCast), prefabName);
            baseSkill = go.AddComponent(script) as Base_Skill;
            baseSkill.skill = skill;
            baseSkill.anime = go.AddComponent<Animator>();
            baseSkill.anime.runtimeAnimatorController = animeController;
            BoxCollider2D boxCollider = go.AddComponent<BoxCollider2D>();
            boxCollider.isTrigger = true;
            yield return new WaitForSeconds(skill.skillCoolTime);
        }
    }
}