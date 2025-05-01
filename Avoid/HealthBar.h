#pragma once
#include "cocos2d.h"
#include "ui/UIScale9Sprite.h"

class HealthBar : public cocos2d::Node
{
public:
    virtual bool init() override;
    void setHealthBar(float _value); // _value: 0.0 ~ 1.0

    CREATE_FUNC(HealthBar);

private:
    cocos2d::ui::Scale9Sprite* healthBarFill;
    cocos2d::ui::Scale9Sprite* healthBarFrame;

    cocos2d::Size healthBarSize;
    float percent = 1.0f;
};
