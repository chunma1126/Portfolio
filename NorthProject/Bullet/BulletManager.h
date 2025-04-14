#pragma once
#include "Scene.h"

class Object;
class Projectile;

class BulletManager
{
	DECLARE_SINGLE(BulletManager);
public:
	void Init();
	void Update();
	void ResetSpinShot();
	void ResetRoseShot();
	void RoseSpinShotComplete(Projectile* item);
	void BasicShot(Vec2 _pos, Scene* _scene, float _bulletSpeed, Vec2 _dir);//
	void CircleShot(Vec2 _pos, Scene* _scene, float _interval, float _bulletSpeed);//
	void CircleShotGoToTarget(Vec2 _pos, Scene* _scene, float _interval, float _bulletSpeed, Object* _target, float _changeTime);//
	void ShapeShot(Vec2 _pos, Scene* _scene, float _bulletSpeed, Vec2 _dir, int _vertex, float _size, float _rotationSpeed);//

#pragma region Spinshot
	void SpinShot(Vec2 _pos, Scene* _scene, float _interval, float _bulletSpeed, float& _spinAngle, float _turnSpeed, float _endTime);//
	void ApplySpinShot();//
#pragma endregion

#pragma region HeartShot
	void HeartShot(Vec2 _pos, Scene* _scene, float _bulletSpeed);
	void HeartShotGoToTarget(Vec2 _pos, Scene* _scene, float _bulletSpeed, Object* _target, float _changeTime);//
	void HeartDataInit(float _rotation);//
#pragma endregion

#pragma region RoseShot
	void RoseShot(Scene* _scene, float _interval, float _bulletSpeed, Vec2 _dir, int _petals, float _size);//
	void RoseSpinShot(Vec2 _pos, Scene* _scene, float _interval, float _bulletSpeed, Object* _target, int _petals, float _size, float _endTime, float _rotationSpeed);
	void ApplyRoseSpinShot();
#pragma endregion

private:
	Object* m_target;

#pragma region spinshot
	Vec2 m_spinShotPos;
	Scene* m_spinShotScene;
	float m_spinShotInterval;
	float m_spinShotBulletSpeed;

	bool m_isSpinShot;

	float m_spinTurnSpeed;
	float m_spinEndTime;
	float m_spinAngle;

	float m_spinShotTimer;
	float m_spinEndTimer;
#pragma endregion

	// HeartShot
	Vec2 m_heartShotPos;
	Scene* m_heartShotScene;
	float m_heartShotInterval;
	float m_heartShotBulletSpeed;

	float m_heartRotation;
private:
	float m_speeds[34];
	float m_direction[34];

	// rose spinShot
private:
	Vec2 m_roseShotPos;
	Scene* m_roseShotScene;
	float m_roseShotInterval;
	float m_roseShotBulletSpeed;

	vector<Projectile*> m_roseVec;
	Vec2 m_roseDir;

	bool m_isRoseShot;
	bool m_roseShotEnd;

	int m_rosePetals;
	int m_roseCount;

	float m_roseEndTime;
	float m_roseEndTimer;
	float m_roseRotationSpeed;
	float m_roseShotTimer;
	float m_roseSize;
};
