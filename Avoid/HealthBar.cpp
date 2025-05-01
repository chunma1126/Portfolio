#include "HealthBar.h"

USING_NS_CC;

#define FRAME_COLOR 220, 221, 225
#define FILL_COLOR 229, 80, 57

bool HealthBar::init()
{
    if (!Node::init()) return false;

    healthBarSize = Size(250, 17);

    healthBarFrame = ui::Scale9Sprite::create("HealthBar.png");
    healthBarFrame->setContentSize(healthBarSize);
    healthBarFrame->setCapInsets(Rect(1, 1, 1, 1));

    healthBarFrame->setColor(Color3B(FRAME_COLOR));

    addChild(healthBarFrame,-1);

    healthBarFill = ui::Scale9Sprite::create("HealthBar.png");
    healthBarFill->setContentSize(healthBarSize);
    healthBarFill->setCapInsets(Rect(1, 1, 1, 1));

    healthBarFill->setAnchorPoint({0,0.5f});
    healthBarFill->setPositionX(getPositionX() - healthBarSize.width / 2);

    healthBarFill->setColor(Color3B(FILL_COLOR));

    addChild(healthBarFill);

    return true;
}

void HealthBar::setHealthBar(float _value)
{
    percent = clampf(_value, 0.0f, 1.0f);

    auto scaleAction = ScaleTo::create(0.7f,percent, 1.0f);
    auto easeAction = EaseExponentialOut::create(scaleAction);
    
    healthBarFill->runAction(easeAction);
}
