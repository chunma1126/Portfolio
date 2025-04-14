#pragma once
class Camera
{
public:
	DECLARE_SINGLE(Camera);
public:

	void Init();

	const Vec2& GetCameraPos() const
	{
		return m_camerapos;
	}
	void SetCameraPos(Vec2 _pos)
	{
		m_camerapos = _pos;
	}
	const Vec2& GetScreenPos(const Vec2& _worldPos) const
	{
		return{ _worldPos.x - m_camerapos.x + SCREEN_WIDTH / 2,
					_worldPos.y - m_camerapos.y + SCREEN_HEIGHT / 2 };
	}
private:
	Vec2 m_camerapos = { 0,0 };
	float m_cameraScale = 1.f;


};

