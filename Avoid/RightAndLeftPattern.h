#pragma once
#include "ArrowPattern.h"
#include "cocos2d.h"

class RightAndLeftPattern : public ArrowPattern 
{
public:
	virtual ~RightAndLeftPattern();
	void start() override;
	void update(float dt) override;
	void reset() override;
	bool isCompleted() override;
private:
	cocos2d::Vec2 _rightStartPos;
	cocos2d::Vec2 _leftStartPos;
private:
	const int _arrowInterval = 55;
	const float _leftArrowFireTime = 1.5f;
	
	bool _canLeftFire = true;
};

