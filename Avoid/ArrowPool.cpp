#include "ArrowPool.h"
#include "Arrow.h"

USING_NS_CC;


Arrow* ArrowPool::pop(Vec2 _pos)
{
	if (pool.empty())
	{
		createPool(10);
	}

	Arrow* newArrow = pool.top();

	newArrow->fadeIn();
	pool.pop();

	
	newArrow->setVisible(true);
	newArrow->setPosition(_pos);

	scene->addChild(newArrow,0);

	return newArrow;
}

Arrow* ArrowPool::pop(Vec2 _pos, Vec2 _direction, float speed)
{
	Arrow* arrow = pop(_pos);
	arrow->setDirectionAndSpeed(_direction, speed);
	return arrow;
}

void ArrowPool::push(Arrow* arrow)
{
	arrow->setVisible(false);
	arrow->setIngnoreRotate(false);
	arrow->removeFromParentAndCleanup(false);
	arrow->setDirectionAndSpeed(Vec2::ZERO, 0);
	arrow->fadeOut();
	arrow->stopAllActions();

	pool.push(arrow);
}

void ArrowPool::createPool(int count)
{
	while (!pool.empty()) pool.pop();

	for (int i = 0; i < count; ++i)
	{
		Arrow* arrow = Arrow::create();
		arrow->retain();
		arrow->setVisible(false);
		arrow->setDirectionAndSpeed(Vec2::ZERO, 0);
		pool.push(arrow);
	}
}

void ArrowPool::releasePool()
{
	while (!pool.empty())
	{
		Arrow* arrow = pool.top();
		pool.pop();
		arrow->removeFromParentAndCleanup(true);
		arrow->release();
	}
}
