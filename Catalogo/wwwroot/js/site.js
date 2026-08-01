const canvas = document.createElement("canvas");
canvas.id = "fondoCanvas";
document.body.appendChild(canvas);

const ctx = canvas.getContext("2d");

let width = window.innerWidth;
let height = window.innerHeight;

canvas.width = width;
canvas.height = height;

window.addEventListener("resize", () => {
    width = window.innerWidth;
    height = window.innerHeight;

    canvas.width = width;
    canvas.height = height;
});

const particles = [];

for (let i = 0; i < 80; i++) {
    particles.push({
        x: Math.random() * width,
        y: Math.random() * height,
        radius: Math.random() * 2 + 1,
        speedX: (Math.random() - 0.5) * 1.5,
        speedY: (Math.random() - 0.5) * 1.5
    });
}

function drawParticles() {

    ctx.clearRect(0, 0, width, height);

    particles.forEach(p => {

        ctx.beginPath();
        ctx.arc(p.x, p.y, p.radius, 0, Math.PI * 2);

        ctx.fillStyle = "#ff7a00";
        ctx.fill();

        p.x += p.speedX;
        p.y += p.speedY;

        if (p.x < 0 || p.x > width)
            p.speedX *= -1;

        if (p.y < 0 || p.y > height)
            p.speedY *= -1;
    });

    connectParticles();

    requestAnimationFrame(drawParticles);
}

function connectParticles() {

    for (let a = 0; a < particles.length; a++) {

        for (let b = a; b < particles.length; b++) {

            const dx = particles[a].x - particles[b].x;
            const dy = particles[a].y - particles[b].y;

            const distance = Math.sqrt(dx * dx + dy * dy);

            if (distance < 120) {

                ctx.beginPath();

                ctx.strokeStyle = "rgba(255,122,0,0.15)";
                ctx.lineWidth = 1;

                ctx.moveTo(particles[a].x, particles[a].y);
                ctx.lineTo(particles[b].x, particles[b].y);

                ctx.stroke();
            }
        }
    }
}

drawParticles();

window.addEventListener("load", () => {
    const splash = document.getElementById("splash");

    if (!splash)
        return;

    splash.classList.add("splash--hidden");

    setTimeout(() => {
        splash.remove();
    }, 450);
});
