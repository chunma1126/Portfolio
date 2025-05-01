#include "UIController.h"
#include "GameScene.h"
#include "TitleScene.h"
#include <string>
USING_NS_CC;

#define BLOOD_SCREEN_INCREASE increaseTime, 70
#define BLODD_SCREEN_DECREASE decreaseTime, 0

bool UIController::init()
{
	if (Node::init() == false)
		return false;
	

	auto visibleSize = Director::getInstance()->getVisibleSize();
	Vec2 origin = Director::getInstance()->getVisibleOrigin();
	screenCenter = Vec2(visibleSize.width / 2 + origin.x, visibleSize.height / 2 + origin.y);

	//timeLabel init
	{
		_timeCount.init();
		_timeView.init();
	}

	//bloodScreen init
	{
		_bloodScreen = cocos2d::Sprite::create("bloodScreen.png");
		_bloodScreen->setPosition(screenCenter);

		float scaleX = visibleSize.width / _bloodScreen->getContentSize().width;
		float scaleY = visibleSize.height / _bloodScreen->getContentSize().height;

		_bloodScreen->setScale(scaleX, scaleY);
		_bloodScreen->setVisible(false);
		_bloodScreen->setOpacity(0);
	}

	// HealthBar init
	{
		_healthBar = HealthBar::create();
		_healthBar->setPosition({ screenCenter.x, visibleSize.height - 11 });
	}


	scheduleUpdate();

	return true;
}

void UIController::update(float dt)
{
	_timeCount.addTime(dt);
	_timeView.setLabel(_timeCount.getTime());
}

void UIController::setScene(cocos2d::Scene* scene)
{
	_scene = scene;

	_scene->addChild(_timeView.getLabel());
	_scene->addChild(_bloodScreen);
	_scene->addChild(_healthBar);

}

void UIController::playBloodScreen()
{
	_bloodScreen->setVisible(true);

	auto increaseAction = FadeTo::create(BLOOD_SCREEN_INCREASE);
	auto decreaseAction = FadeTo::create(BLODD_SCREEN_DECREASE);

	auto hideAction = CallFunc::create([this]() {
		_bloodScreen->setVisible(false);
		});

	_bloodScreen->runAction(Sequence::create(increaseAction, decreaseAction, hideAction, nullptr));
}

void UIController::setHealthBar(float _value)
{
	_healthBar->setHealthBar(_value);
}

void UIController::playerGameOverScreen(float _playTime)
{
	auto gameOverlabel = cocos2d::Label::createWithSystemFont("Game Over", "fonts/CookieRun Regular.ttf", 45);
	gameOverlabel->setOpacity(255);
	gameOverlabel->setPosition({ screenCenter.x , screenCenter.y + 95});
	_scene->addChild(gameOverlabel,1);

	std::string str = "time : " + std::to_string(_playTime);

	auto playTimeLabel = cocos2d::Label::createWithSystemFont(str, "fonts/CookieRun Regular.ttf", 30);
	playTimeLabel->setPosition({ screenCenter.x , screenCenter.y + 21 });

	_scene->addChild(playTimeLabel, 1);

	auto delay = cocos2d::DelayTime::create(_appearanceTime);

	auto changeScene = cocos2d::CallFunc::create([&]() 
	{
		auto scene = TitleScene::createScene();
		auto transition = cocos2d::TransitionFade::create(_transitionTime, scene);
		cocos2d::Director::getInstance()->replaceScene(transition);
	});

	_scene->runAction(cocos2d::Sequence::create(delay, changeScene, nullptr));
}



