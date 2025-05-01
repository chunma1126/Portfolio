#pragma once
#include "cocos2d.h"
#include "ui/CocosGUI.h" 

class TitleScene : cocos2d::Scene
{
public:
    static cocos2d::Scene* createScene();

    virtual bool init();
    virtual void onEnter() override;
    CREATE_FUNC(TitleScene);
private:
    const float _titleLabelFadeInTime = 0.6f;
    const float _titleLabelFadeOutTime = 0.6f;

    const int _fadeInAlpha = 230;
    const int _fadeOutAlpha = 66;

    const float _transitionTime = 0.4f;

};

