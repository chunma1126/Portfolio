#include "TimeView.h"

USING_NS_CC;

void TimeView::init()
{
	timeLabel = Label::createWithSystemFont("time", "fonts/CookieRun Regular.ttf",25);

	auto visibleSize = Director::getInstance()->getVisibleSize();
	Vec2 origin = Director::getInstance()->getVisibleOrigin();
	Vec2 screenCenter = Vec2(visibleSize.width / 2 + origin.x, visibleSize.height / 2 + origin.y + 120);

	timeLabel->setPosition(screenCenter);
}
