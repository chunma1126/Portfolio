#pragma once
#include "ArrowPattern.h"

class Arrow;
class SquarePattern : public  ArrowPattern
{
public:
	virtual ~SquarePattern();
	void start() override;
	void update(float dt) override;
	void reset() override;
	bool isCompleted() override;
private:
	float suqareWidth = 100;
	float suqareHeight = 100;

	ArrowInfo _spawnInfo[4];
	float _spawnInterval = 0.7f;
	int _spawnIndex = 0;

	int _randomIndex[4] = {0,1,2,3};
	float _arrowInterval = 0.14f;

	float _completeTimer = 0;
	float _sectorPatternTimer = 0;
	const int _sectorPatternCount = 5;
};

