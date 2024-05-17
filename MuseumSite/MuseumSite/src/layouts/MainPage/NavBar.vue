<template>
<nav class="navbar navbar-expand-lg fixed-top bg-light navbar-light">
  <div class="container">
    <a class="navbar-brand gap-3 d-flex flex-row p-2" href="#"
      ><img
        id="logo"
        src="../../img/publicmuseumsign_89226.png"
        alt="Logo"
        draggable="false"
        height="40"
    /><p class="m-1">Гусятинський краєзнавчий музей</p></a>
    <button
      class="navbar-toggler"
      type="button"
      data-mdb-toggle="collapse"
      data-mdb-target="#navbarSupportedContent"
      aria-controls="navbarSupportedContent"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <i class="fas fa-bars"></i>
    </button>
    <div class="collapse navbar-collapse" id="navbarSupportedContent">
      <ul class="navbar-nav ms-auto align-items-center">
        <li class="nav-item">
          <a class="nav-link mx-2 fw-bold" href="#!"><i class="fas fa-plus-circle pe-2"></i>Головна</a>
        </li>
        <li class="nav-item">
          <a class="nav-link mx-2 fw-bold" href="#!"><i class="fas fa-bell pe-2"></i>Alerts</a>
        </li>
        <li class="nav-item">
          <a class="nav-link mx-2 fw-bold" href="#!"><i class="fas fa-heart pe-2"></i>Trips</a>
        </li>
        <li class="nav-item ms-3">
          <a class="btn btn-outline-dark" href="#!">Sign in</a>
        </li>
      </ul>
    </div>
  </div>
</nav>
</template>