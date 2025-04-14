#pragma once
class Texture;
class UI
{
public:
	UI();
	virtual ~UI();

public:
	virtual void Init();
	virtual void Update();
	virtual void Render(HDC _dc);

public:
	const Vec2& GetPos() const { return m_vPos; }
	const Vec2& GetSize() const { return m_vSize; }
	
public:
	virtual void SetPos(Vec2 _vPos) { m_vPos = _vPos; }
	virtual void SetSize(Vec2 _vSize) { m_vSize = _vSize; }
	virtual void SetTexture(Texture* _tex) { m_pTexture = _tex; }
	virtual void SetActive(bool  _active) { m_active = _active; }
protected:
	Vec2 m_vPos;
	Vec2 m_vSize;
	Texture* m_pTexture;
	bool m_active = false;
};

