#pragma once
#include "ArrowPattern.h"
class ForkPattern : public ArrowPattern
{
public:
	virtual ~ForkPattern();
	void start() override;
	void update(float dt) override;
	void reset() override;
	bool isCompleted() override;
private:
	float _completeTimer = 0;

	float _startPosX;
	float _endPosX;
	std::vector<Vec2> _arrowPositions;

	float _arrowInterval = 0.1f;
	int _spawnIndex = 0;
	
	float _horizontalMargin = 25;
};

