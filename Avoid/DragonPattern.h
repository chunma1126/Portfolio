#pragma once
#include "ArrowPattern.h"
class DragonPattern : public ArrowPattern
{
public:
	virtual ~DragonPattern();
	void start() override;
	void update(float dt) override;
	void reset() override;
	bool isCompleted() override;
private:
	float _amplitude = 80; 
	float _frequency = 3.f;
	float _margin = 60;

	float _fireTimer = 0;
	float _fireTime = 0.1;

	Size visibleSize;
	Vec2 screenCenter;
};

