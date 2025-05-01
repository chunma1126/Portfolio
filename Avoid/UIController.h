#pragma once
#include "cocos2d.h"
#include "TimeCount.h"
#include "TimeView.h"
#include "HealthBar.h"

class UIController : public cocos2d::Node
{
public:
	virtual bool init();
	virtual void update(float dt);
	void setScene(cocos2d::Scene* scene);
	void playBloodScreen();
	void setHealthBar(float _value);
	void playerGameOverScreen(float _playTime);

	TimeCount getTimeCount(){ return _timeCount; }

	CREATE_FUNC(UIController);
private:
	TimeCount _timeCount;
	TimeView _timeView;
	HealthBar* _healthBar;

	cocos2d::Scene* _scene;
	cocos2d::Sprite* _bloodScreen;
	cocos2d::Vec2 screenCenter = {};
private:
	float increaseTime = 0.13f;
	float decreaseTime = 1.02f;

	float _transitionTime = 1.2f;
	float _appearanceTime = 1.4f;

};

