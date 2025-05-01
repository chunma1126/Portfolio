#pragma once
#include "ArrowPattern.h"
enum SECTOR_DIRECTION 
{
	DOWN = 0,
	RIGHT = 1,
	UP = 2,
	LEFT = 3,
};
class SectorPattern : public ArrowPattern
{
public:
	SectorPattern(int direction) { _direction = direction; };
	virtual ~SectorPattern();
	void start() override;
	void update(float dt) override;
	void reset() override;
	bool isCompleted() override;
private:
	int _direction = 0;
	float _margin = 100;
};

