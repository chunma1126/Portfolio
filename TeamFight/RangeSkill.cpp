#include "RangeSkill.h"
#include "Entity.h"
#include "Archer.h"

void RangeSkill::execute(Entity* caster, Entity* target)
{
    Vec2 originalPos = caster->getPosition();
    Vec2 enemyPos = target->getPosition();
    Vec2 movePos = { enemyPos.x - enemyPos.x / 3 , enemyPos.y };

    auto archer = static_cast<Archer*>(caster);

    // 1. �� ������ �̵�
    auto moveToTarget = MoveTo::create(0.35f, movePos);

    // 2. ���� �ִϸ��̼� ����
    auto playAttackAnimation = CallFunc::create([caster]() {
        caster->playAnimation(ANIMATION_STATE::ATTACK1, false);
        });

    // 3. ȭ�� �߻� �ݹ�
    auto shootArrow = CallFunc::create([archer, enemyPos]() {
        auto arrow = archer->getArrow();
        arrow->setVisible(true);
        arrow->setAnchorPoint(Vec2(0.5f, 0.5f));

        Vec2 startWorldPos = arrow->getParent()->convertToWorldSpace(arrow->getPosition());
        Vec2 direction = enemyPos - startWorldPos;
        float angle = CC_RADIANS_TO_DEGREES(atan2(direction.y, direction.x));
        arrow->setRotation(-angle);

        Vec2 targetLocalPos = arrow->getParent()->convertToNodeSpace(enemyPos);

        auto moveArrow = MoveTo::create(0.24f, targetLocalPos);

        auto hideArrow = CallFunc::create([archer]() {
            auto arrow = archer->getArrow();
            arrow->setVisible(false);
            arrow->setPosition(archer->getArrowPosition());
            });

        auto arrowSequence = Sequence::create(moveArrow, hideArrow, nullptr);
        arrow->runAction(arrowSequence);
        });

    // 4. ���� ���� �ݹ�
    auto applyDamageToTarget = CallFunc::create([this, caster, target]() {
        applyDamage(caster, target);
        });

    // 5. ���� �ڸ��� ����
    auto moveBackToOrigin = MoveTo::create(0.4f, originalPos);

    // 6. ��ü ������ ����
    auto sequence = Sequence::create(
        moveToTarget,
        playAttackAnimation,
        DelayTime::create(0.6f),  
        shootArrow,
        DelayTime::create(0.24f),  
        applyDamageToTarget,
        DelayTime::create(0.2f),  
        moveBackToOrigin,
        nullptr
    );

    caster->runAction(sequence);
}
