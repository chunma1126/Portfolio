#pragma once
#include <stack>
#include "Arrow.h"

class ArrowPool {
public:
	static ArrowPool& getInstance() {
		static ArrowPool instance;
		return instance;
	}

	void initialize(int _count, cocos2d::Scene* _scene) {
		scene = _scene;
		createPool(_count);
	}

	Arrow* pop(Vec2 _pos);
	Arrow* pop(Vec2 _pos,Vec2 _direction,float speed);
	void push(Arrow* arrow);

private:
	ArrowPool() = default;
	~ArrowPool() 
	{
		releasePool();
	}

	ArrowPool(const ArrowPool&) = delete;
	ArrowPool& operator=(const ArrowPool&) = delete;

	void createPool(int count);
	void releasePool();

	std::stack<Arrow*> pool;
	cocos2d::Scene* scene = nullptr;
};


